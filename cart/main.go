package main

import (
    // "encoding/json"
    "log"
    "net/http"
		"context"
    "github.com/gorilla/mux"
		"github.com/go-redis/redis/v9"
		// Database
		"cart/db"
)

// Struct for each product in the cart
// type Product struct {
// 	ProductID string `json:"productId"`
// 	UserID string `json:"userId"`
// 	Quantity int8 `json:"quantity"`
// }

var (
	ListenAddr = "localhost:8080"
	RedisAddr = "cart-redis:6379"
)



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
		database, err := db.NewDatabase(RedisAddr)
		if err != nil {
			log.Fatalf("Failed to connect to redis: %s", err.Error())
		}
	
    router := mux.NewRouter()

		router.HandleFunc("/cart/{userId}", getCart).Methods("GET")
		router.HandleFunc("/cart/{userId}", addToCart).Methods("POST")
		router.HandleFunc("/cart/{userId}", deleteCart).Methods("DELETE")


    log.Fatal(http.ListenAndServe(":8000", router))
}

