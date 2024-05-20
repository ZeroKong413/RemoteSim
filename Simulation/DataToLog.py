from PIL import Image
import os

def convert_180_to_360(input_image_path, output_image_path):
    # 180도 이미지 불러오기
    input_image = Image.open(input_image_path)
    
    # 이미지 크기 및 채우기 색상 설정
    width, height = input_image.size
    fill_color = (0, 0, 0)  # 검정색 배경

    # 180도 이미지를 새로운 이미지 객체로 변환
    output_image = Image.new('RGB', (width * 2, height), fill_color)
    output_image.paste(input_image, (0, 0))

    # 이미지 저장
    output_image.save(output_image_path)

# 여러 이미지를 처리하는 함수
def convert_multiple_images(images_folder_path, output_folder_path):
    for filename in os.listdir(images_folder_path):
        if filename.endswith(('.png', '.jpg', '.jpeg')):
            input_image_path = os.path.join(images_folder_path, filename)
            output_image_path = os.path.join(output_folder_path, filename)
            output_image_path = output_image_path.replace(".", "_360.")
            convert_180_to_360(input_image_path, output_image_path)

# 사용 예시
images_folder_path = "Image_F"
output_folder_path = "360_Iamge_F"
convert_multiple_images(images_folder_path, output_folder_path)
