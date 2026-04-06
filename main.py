from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()

items = []

class Item(BaseModel):
    name: str
    price: float

@app.get("/")
def home():
    return {"message": "Hello from FastAPI!"}

@app.get("/items/{item_id}")
def get_item(item_id: int):
    if 0 <= item_id < len(items):
        return items[item_id]
    return {"error": "Item not found"}

@app.post("/items")
def create_item(item: Item):
    items.append(item.dict())
    return {"status": "Item added", "item": item.dict()}