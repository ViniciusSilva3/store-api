package main

import (
	"testing"
	"cart/db"
	"github.com/google/uuid"
	"net/http"
	"net/http/httptest"
	"encoding/json"
	"bytes"
)

var test_product = db.Product{
	ProductId: uuid.New().String(),
	UserId: uuid.New().String(),
	Quantity: 1,
}

var test_product2 = db.Product{
	ProductId: uuid.New().String(),
	UserId: test_product.UserId,
	Quantity: 1,
}

func startDB() *db.Database {
	db, err := db.NewDatabase("cart-redis:6379")
	if err != nil {
		panic(err)
	}
	return db
}

func startApp() *App {
	a := App{}
	a.Initialize("cart-redis:6379", ":8080")
	return &a
}

func populateCart() {
	db := startDB()
	db.AddToCart(&test_product)
	db.AddToCart(&test_product2)
}

func executeRequest(req *http.Request) *httptest.ResponseRecorder {
	rr := httptest.NewRecorder()
	a := startApp()
	a.Router.ServeHTTP(rr, req)

	return rr
}

func checkResponseCode(t *testing.T, expected, actual int) {
	if expected != actual {
			t.Errorf("Expected response code %d. Got %d\n", expected, actual)
	}
}

func isProductInProducts(product db.Product, products []db.Product) (result bool) {
	result = false
	for _, product := range products {
		if test_product == product {
			result = true
			break
		}
	}
	return result
}

func TestGetCart(t *testing.T) {
	populateCart()
	req, _ := http.NewRequest("GET", "/cart/" + test_product.UserId, nil)
	response := executeRequest(req)

	data := []db.Product{}
	json.Unmarshal([]byte(response.Body.String()), &data)

	if !isProductInProducts(test_product, data) {
		t.Error("Product" + test_product.ProductId + " not added to cart")
	}

	if !isProductInProducts(test_product2, data) {
		t.Error("Product" + test_product2.ProductId + " not added to cart")
	}
}

func TestAddToCart(t *testing.T) {
	body, _ := json.Marshal(test_product)
	
	req, _ := http.NewRequest("POST", "/cart/" + test_product.UserId, bytes.NewBuffer(body))
	response := executeRequest(req)

	checkResponseCode(t, http.StatusNoContent, response.Code)
}

func TestDeleteProductFromCart(t *testing.T) {
	populateCart()
	req, _ := http.NewRequest("DELETE", "/cart/" + test_product.UserId + "/" + test_product.ProductId, nil)
	response := executeRequest(req)

	checkResponseCode(t, http.StatusNoContent, response.Code)

	req, _ = http.NewRequest("GET", "/cart/" + test_product.UserId, nil)

	response = executeRequest(req)
	data := []db.Product{}
	json.Unmarshal([]byte(response.Body.String()), &data)

	if len(data) != 1 {
		t.Error("Product not deleted from cart")
	}

	if data[0] != test_product2 {
		t.Error("Product not deleted from cart")
	}

	if isProductInProducts(test_product, data) {
		t.Error("Product not deleted from cart")
	}
}

func TestDeleteCart(t *testing.T) {
	populateCart()
	req, _ := http.NewRequest("DELETE", "/cart/" + test_product.UserId, nil)
	response := executeRequest(req)

	checkResponseCode(t, http.StatusNoContent, response.Code)

	req, _ = http.NewRequest("GET", "/cart/" + test_product.UserId, nil)

	response = executeRequest(req)
	data := []db.Product{}
	json.Unmarshal([]byte(response.Body.String()), &data)

	if len(data) != 0 {
		t.Error("Cart not deleted")
	}
}
