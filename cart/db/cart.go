package db

import (
	"fmt"
	"strings"
	"strconv"
)

var cartKey = "cart:" 

type Product struct {
	ProductId string `json:"productId"`
	UserId string `json:"userId"`
	Quantity int `json:"quantity"`
}

func (db *Database) GetCart(userId string) ([]Product, error) {
	cartKey := cartKey + strings.TrimSpace(userId)
	val, err := db.Client.HGetAll(Ctx, cartKey).Result()
	if err != nil {
		fmt.Println(err)
		return nil, err
	}
	var products []Product
	for k, v := range val {
		product := Product{}
		product.ProductId = k
		product.UserId = strings.TrimSpace(userId)
		product.Quantity, _ = strconv.Atoi(v)
		products = append(products, product)
	}
	return products, nil
}

func (db *Database) AddToCart(product *Product) error {
	cartKey := cartKey + strings.TrimSpace(product.UserId)

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

