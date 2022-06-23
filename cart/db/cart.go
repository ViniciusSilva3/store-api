package db

import (
	"fmt"
	"strings"
)

var cartKey = "cart:" 

type Product struct {
	ProductId string `json:"productId"`
	UserId string `json:"userId"`
	Quantity int8 `json:"quantity"`
}

func (db *Database) GetCart(userId string) ([]Product, error) {

	return []Product{}, nil
}

func (db *Database) AddToCart(product *Product) error {
	fmt.Print("product: ", product.UserId)
	cartKey := cartKey + strings.TrimSpace(product.UserId)
	fmt.Print("key: ", cartKey)

	err := db.Client.HSet(Ctx, cartKey, strings.TrimSpace(product.ProductId), product.Quantity)
	if err != nil {
		fmt.Println(err)
	}
	return nil
}

func (db *Database) RemoveItemFromCart(product *Product) error {
	var key = cartKey + product.UserId + ":" + product.ProductId
	err := db.Client.Del(Ctx, key)
	if err != nil {
		panic(err)
	}
	return nil
}

func (db *Database) DeleteCart(userId string) error { 
	var key = cartKey + userId + ":*"
	err := db.Client.Del(Ctx, key)

	if err != nil {
		panic(err)
	}
	return nil
}

