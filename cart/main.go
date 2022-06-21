package main

import (
    // "encoding/json"
    "log"
    "net/http"
    "github.com/gorilla/mux"
)

// Redis.sadd('cart-#{userId}', Product)

// Struct for each product in the cart
type Product struct {
	ProductID string `json:"productId"`
	UserID string `json:"userId"`
	Quantity int8 `json:"quantity"`
}


func getCart(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	// get the hset from Redis
}

func addToCart(w http.ResponseWriter, r *http.Request) {
	// add items to cart
}

func deleteCart(w http.ResponseWriter, r *http.Request) {
	// delete cart
}



func main() {
    router := mux.NewRouter()

		router.HandleFunc("/cart/{userId}", getCart).Methods("GET")
		router.HandleFunc("/cart/{userId}", addToCart).Methods("POST")
		router.HandleFunc("/cart/{userId}", deleteCart).Methods("DELETE")


    log.Fatal(http.ListenAndServe(":8000", router))
}
