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

func (db *Database) DeleteCart(userId string) error { 
	var key = cartKey + userId + ":*"
	err := db.Client.HDel(Ctx, key)

	if err != nil {
		panic(err)
	}
	return nil
}

func (db *Database) DeleteProductFromCart(userId string, productId string) error { 
	cartKey := cartKey + strings.TrimSpace(userId)
	fmt.Print("cartKey: ", cartKey)
	fmt.Print("productId: ", productId)

	err := db.Client.HDel(Ctx, cartKey, strings.TrimSpace(productId))

	if err != nil {
		fmt.Print(err)
	}
	return nil
}

