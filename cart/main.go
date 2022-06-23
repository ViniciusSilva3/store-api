// main.go

package main

var (
	ListenAddr = "localhost:8080"
	RedisAddr = "cart-redis:6379"
)

func main() {
		a := App{}
		a.Initialize(RedisAddr, ListenAddr)
		a.Run(ListenAddr)
}
