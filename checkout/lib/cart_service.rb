class CartService

  def initialize(user_id)
    @user_id = user_id
  end

  def api_connection()
    Faraday.new(url: ENV.fetch('CART_URL')) do |conn|
      conn.request :json
      conn.response :json
      conn.adapter Faraday.default_adapter
    end
  end

  def get
    api_connection().get do |request|
      request.url "cart/#{@user_id}"
    end
  end
end
