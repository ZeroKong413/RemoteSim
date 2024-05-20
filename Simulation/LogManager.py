from pyevsim import BehaviorModelExecutor, SystemSimulator, Infinite
from DataClass import DataClassModel
from UnityCommunication import CommunicationModel

class LogManager():
    def __init__(self) -> None:
        # print(data)
        self.us = SystemSimulator()

        self.us.register_engine("COM", "VIRTUAL_TIME", 1)

        self.unity_model = self.us.get_engine("COM")

        self.unity_model.insert_input_port("start")
        
        print("start engine")
        Communication_m = CommunicationModel(0, Infinite, "Communication_m", "COM")
        DataClass_m = DataClassModel(0, Infinite, "DataClass_m", "COM")

        self.unity_model.register_entity(Communication_m)
        self.unity_model.register_entity(DataClass_m)

        self.unity_model.coupling_relation(None, "start", DataClass_m, "start")
        self.unity_model.coupling_relation(DataClass_m, "image_data", Communication_m, "start")
        self.unity_model.coupling_relation(Communication_m, "control_data", DataClass_m, "start")
   
        self.start()


    def start(self) -> None:
        # pass
        self.unity_model.insert_external_event("start","start")
        self.unity_model.simulate()

if __name__ == '__main__':
    Log_manager = LogManager()