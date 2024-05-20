from pyevsim import BehaviorModelExecutor, Infinite, SysMessage
import zmq
import time
import socket


class CommunicationModel(BehaviorModelExecutor):
    def __init__(self, instance_time, destruct_time, name, engine_name):
        BehaviorModelExecutor.__init__(self, instance_time, destruct_time, name, engine_name)

        self.init_state("Wait")
        self.insert_state("Wait", Infinite)
        self.insert_state("Generate",1)

        self.insert_input_port("start")

        self.insert_output_port("image_data")

        self.one_time_setup()

    def ext_trans(self, port, msg):
        if port == "start":
            print("unitycommunication start in")
            self.image_data_one = msg.retrieve()[0][0]
            self._cur_state = "Generate"


    def output(self):
        if self._cur_state == "Generate":
            print(len(self.image_data_one))
            self.conn.send(self.image_data_one)

            self._cur_state = "Wait"

            # zmq code
            # self.send_to_unity(self.image_data_one)
            

        if self._cur_state == "Wait" :

            raw_data = self.conn.recv(4096).decode("utf-8")
            # zmq code
            # if self.received_to_unity():
            #     print("unity message in python")
            #     message = self.received_to_unity()
            print(raw_data)
           
            msg = SysMessage(self.get_name(), "control_data")
            msg.insert([raw_data])
            return msg


    
    def int_trans(self):
        if self._cur_state == "Wait":
            self._cur_state = "Wait"


    def one_time_setup(self):
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.socket.bind(("0.0.0.0", 11013))
        self.socket.listen(5)

        # while True:
        print("Wait Unity Socket Client")
        self.conn, self.addr = self.socket.accept()
        print(f" * {self.addr[0]} 연결")

        # zmq code
        # self.context = zmq.Context()
        # self.set_publisher()
        # self.set_subscriber()

   


    # zmq code

    # def received_to_unity(self):
    #     try:
    #         while True:
    #             message = self.subscriber.recv_string()
    #             print(f"Received message: {message}")
    #             return message
    #     except KeyboardInterrupt:
    #         print("Program interrupted")

    # Unity Publisher -> python Subscriber
    # def set_subscriber(self):
    #     print("set subscriber zmq")
    #     self.subscriber = self.context.socket(zmq.SUB)
    #     self.subscriber.connect("tcp://localhost:11011")
    #     self.subscriber.setsockopt_string(zmq.SUBSCRIBE, '')


    #### Unity Subscirber <- python Publisher
    # def set_publisher(self):
    #     print("set publisher zmq")
    #     self.publisher = self.context.socket(zmq.PUB)
    #     self.publisher.bind("tcp://*:11012")

    # def send_to_unity(self, message):
    #     self.publisher.send(message)
        