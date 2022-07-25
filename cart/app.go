//app.go

package main

import (
	"github.com/gorilla/mux"
	"cart/db"
	"log"
	"net/http"
	"encoding/json"
	"fmt"
)

type App struct {
	Router *mux.Router
	DB     *db.Database
}

func (a *App) Initialize(RedisAddr string, ListenAddr string) {
	var err error
	fmt.Print("Starting db...")
	a.DB, err = db.NewDatabase(RedisAddr)
	if err != nil {
		log.Fatalf("Failed to connect to redis: %s", err.Error())
	}

	a.Router = mux.NewRouter()
	a.initializeRoutes()
}

func (a *App) GetCart(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	// Get the HSet from Redis
	params := mux.Vars(r)
	userId := params["userId"]
	cart, err := a.DB.GetCart(userId)
	if err != nil {
		log.Println(err)

		w.WriteHeader(http.StatusInternalServerError)
		return
	}

	json.NewEncoder(w).Encode(cart)

	// return the cart
	w.WriteHeader(http.StatusOK)
		
}

func (a *App) AddToCart(w http.ResponseWriter, r *http.Request) {
	// Add items to cart
	params := mux.Vars(r)
	var product db.Product
	decoder := json.NewDecoder(r.Body)

	if err := decoder.Decode(&product); err != nil {
		w.WriteHeader(http.StatusBadRequest)
		fmt.Print("Error decoding JSON: ", err)
		return
	}
	defer r.Body.Close()
	product.UserId = params["userId"]
	if err := a.DB.AddToCart(&product); err != nil {
		return
	}
	w.WriteHeader(http.StatusNoContent)
}

// Clean cart
func (a *App) DeleteCart(w http.ResponseWriter, r *http.Request) {
	params := mux.Vars(r)
	userId := params["userId"]
	if err := a.DB.DeleteCart(userId); err != nil {
		return
	}
	w.WriteHeader(http.StatusNoContent)
}

// Delete an item from the Cart
func (a *App) DeleteProductFromCart(w http.ResponseWriter, r *http.Request) {
	params := mux.Vars(r)
	userId := params["userId"]
	productId := params["productId"]

	if err := a.DB.DeleteProductFromCart(userId, productId); err != nil {
		return
	}

	w.WriteHeader(http.StatusNoContent)
}

func (a *App) initializeRoutes() {
	a.Router.HandleFunc("/cart/{userId}", a.GetCart).Methods("GET")
	a.Router.HandleFunc("/cart/{userId}", a.AddToCart).Methods("POST")
	a.Router.HandleFunc("/cart/{userId}", a.DeleteCart).Methods("DELETE")
	a.Router.HandleFunc("/cart/{userId}/{productId}", a.DeleteProductFromCart).Methods("DELETE")
}

func (a *App) Run(addr string) {
	fmt.Print("Starting server...")

	log.Fatal(http.ListenAndServe(":8000", a.Router))
}

