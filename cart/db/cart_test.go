package db

import (
	"testing"
	"github.com/google/uuid"
)


var test_product = Product{
	ProductId: uuid.New().String(),
	UserId: uuid.New().String(),
	Quantity: 1,
}

func isProductInProducts(product Product, products []Product) (result bool) {
	result = false
	for _, product := range products {
		if test_product == product {
			result = true
			break
		}
	}
	return result
}

func startDB() *Database {
	db, err := NewDatabase("cart-redis:6379")
	if err != nil {
		panic(err)
	}
	return db
}


func TestAddToCart(t *testing.T) {
	db := startDB()
	db.AddToCart(&test_product)
	cart, err := db.GetCart(test_product.UserId)

	if err != nil {
		t.Error(err)
	}

	if !isProductInProducts(test_product, cart) {
		t.Error("Product not added to cart")
	}
}

func TestGetCart(t *testing.T) {
	db := startDB()

	db.AddToCart(&test_product)
	test_product2 := test_product
	test_product2.ProductId = uuid.New().String()
	db.AddToCart(&test_product2)
	cart, err := db.GetCart(test_product.UserId)

	if err != nil {
		t.Error(err)
	}

	if !isProductInProducts(test_product, cart) {
		t.Error("Product" + test_product.ProductId + " not added to cart")
	}

	if !isProductInProducts(test_product2, cart) {
		t.Error("Product" + test_product2.ProductId + " not added to cart")
	}

	if len(cart) != 2 {

		t.Error("Cart has wrong number of products")
	}
}

func TestDeleteProductFromCart(t *testing.T) {
	db := startDB()

	db.AddToCart(&test_product)
	db.DeleteProductFromCart(test_product.UserId, test_product.ProductId)
	cart, err := db.GetCart(test_product.UserId)

	if err != nil {
		t.Error(err)
	}

	if isProductInProducts(test_product, cart) {
		t.Error("Product" + test_product.ProductId + " not deleted from cart")
	}
}

func TestDeleteCart(t *testing.T) {
	db := startDB()

	db.AddToCart(&test_product)
	db.DeleteCart(test_product.UserId)
	cart, err := db.GetCart(test_product.UserId)

	if err != nil {
		t.Error(err)
	}

	if len(cart) != 0 {
		t.Error("Cart not deleted")
	}
}


