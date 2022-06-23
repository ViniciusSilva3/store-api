// main.go

package main

import (
	"github.com/joho/godotenv"
	"os"
)

func main() {
	godotenv.Load()

	a := App{}
	a.Initialize(
		os.Getenv("REDIS_ADDR"),
		os.Getenv("LISTEN_ADDR"),
	)
	a.Run(os.Getenv("LISTEN_ADDR"))
}
