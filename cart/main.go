package main

import (
    "encoding/json"
    "log"
    "net/http"
		// "context"
    "github.com/gorilla/mux"
		// "github.com/go-redis/redis/v9"
		// Database
		"cart/db"
		"fmt"
)

type App struct {
	Router *mux.Router
	DB     *db.Database
}

// Struct for each product in the cart
// type Product struct {
// 	ProductId string `json:"productId"`
// 	UserId string `json:"userId"`
// 	Quantity int8 `json:"quantity"`
// }

var (
	ListenAddr = "localhost:8080"
	RedisAddr = "cart-redis:6379"
)



func (a *App) GetCart(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	// get the hset from Redis
}

func (a *App) AddToCart(w http.ResponseWriter, r *http.Request) {
	// add items to cart
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
	w.WriteHeader(http.StatusOK)
}

func (a *App) DeleteCart(w http.ResponseWriter, r *http.Request) {
	// delete cart
}

func (a *App) Initialize(RedisAddr string) {
	var err error
	a.DB, err = db.NewDatabase(RedisAddr)
	if err != nil {
		log.Fatalf("Failed to connect to redis: %s", err.Error())
	}

	a.Router = mux.NewRouter()
	a.initializeRoutes()
}

func (a *App) initializeRoutes() {
	a.Router.HandleFunc("/cart/{userId}", a.GetCart).Methods("GET")
	a.Router.HandleFunc("/cart/{userId}", a.AddToCart).Methods("POST")
	a.Router.HandleFunc("/cart/{userId}", a.DeleteCart).Methods("DELETE")
}

func (a *App) Run(addr string) {
	log.Fatal(http.ListenAndServe(":8000", a.Router))
}


func main() {
		a := App{}
		fmt.Print("Starting db...")
		a.Initialize(RedisAddr)
		fmt.Print("Starting server...")
		a.Run(ListenAddr)
}

