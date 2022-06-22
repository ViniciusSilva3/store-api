package db

cartKey = "cart:" 
// cart:userId:productId <quantity> 
// GET cart:userId:* 

type Product struct {
	ProductId string `json:"productId"`
	UserId string `json:"userId"`
	Quantity int8 `json:"quantity"`
}

func (db *Database) GetCart(userId string) (*Product[], error) {
	key = cartKey + userId + ":*"
	return db.Client.Get(Ctx, key)
}

func (db *Database) AddToCart(product *Product) error {
	key = cartKey + product.UserId + ":" + product.ProductId
	db.Client.Set(Ctx, key, product.Quantity)
}

func (db *Database) RemoveItemFromCart(product *Product) error {
	key = cartKey + product.UserId + ":" + product.ProductId
	db.client.Del(Ctx, key)
}

func (db *Database) DeleteCart(userId string) error { 
	key = cartKey + userId + ":*"
	db.client.Del(Ctx, key)
}

