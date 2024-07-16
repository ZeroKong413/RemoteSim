#db
# 1. db를 2개로 나눠서 관리?
# 2. 그냥 MongoDB에 싹 다 넣기
import base64
from datetime import datetime
from pymongo import MongoClient

# MongoDB 클라이언트 설정
client = MongoClient('localhost', 27017)
db = client['robotData']
collection = db['Data']

# 이미지 데이터를 base64로 인코딩
def encode_image(image_path):
    with open(image_path, "rb") as image_file:
        return base64.b64encode(image_file.read()).decode('utf-8')




