from pyevsim import BehaviorModelExecutor, Infinite, SysMessage
import os

class DataClassModel(BehaviorModelExecutor):
    def __init__(self, instance_time, destruct_time, name, engine_name):
        BehaviorModelExecutor.__init__(self, instance_time, destruct_time, name, engine_name)

        self.init_state("Wait")
        self.insert_state("Wait", Infinite)
        self.insert_state("Generate",1)

        self.insert_input_port("start")
        self.insert_input_port("next")

        self.insert_output_port("image_data")

        self.read_images_in_folder("360_Iamge_F")

        self.count_ = 0
 
    def ext_trans(self, port, msg):
        if port == "start":
            print("Data class in")
            self._cur_state = "Generate"

    def output(self):
        if self._cur_state == "Generate":
            if self.count_ >=len(self.images_bytes):
                msg = SysMessage(self.get_name(), "image_data")
                msg.insert([self.images_bytes[len(self.images_bytes)-1][1]])
                return msg
                
            else:
                msg = SysMessage(self.get_name(), "image_data")
                msg.insert([self.images_bytes[self.count_][1]])
                self.count_ += 1
                return msg

    def int_trans(self):
        if self._cur_state == "Generate":
            self._cur_state = "Wait"

    # data log 형태로 변환 해놓기만하는게 필요하긴 할듯( 기능 : Byte 형태로 이미지를 저장)
    def read_png_bytes(self,image_path):
        # 이미지 파일을 바이트로 읽기
        with open(image_path, 'rb') as file:
            png_bytes = file.read()
            return png_bytes


    # def image_classify_by_cmd(self, cmd, image_folder):
    #     # cmd_list = ['d','s','i','up','s','i','s','k','up','k','i','k','i','i','k','j','a','down','l','ctrl']


    #     self.image_data = self.read_png_bytes("converted_360_image.jpg")
    #     return self.image_data
    
    def read_images_in_folder(self, folder_path):
        self.images_bytes = []
        # 폴더 내의 모든 파일들을 검사
        for filename in os.listdir(folder_path):
            # 파일의 절대 경로 구하기
            file_path = os.path.join(folder_path, filename)
            # 파일이 디렉토리인지 확인
            if os.path.isdir(file_path):
                continue  # 디렉토리면 넘어감
            # 파일 이름에 '1'이 포함되어 있으면 건너뜀
            # if '1' in filename:
            #     continue
            # 파일이 이미지인지 확인
            if filename.endswith(('.png', '.jpg', '.jpeg')):
                # 이미지를 바이트로 변환하여 리스트에 추가
                image_bytes = self.read_png_bytes(file_path)
                self.images_bytes.append([0,image_bytes])
        return self.images_bytes