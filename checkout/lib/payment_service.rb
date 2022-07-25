class PaymentService

  def initialize(payment_id: nil, user_id: nil, order_id: nil)
    @user_id = user_id
    @payment_id = payment_id
    @order_id = order_id
  end

  def api_connection()
    Faraday.new(url: ENV.fetch('PAYMENT_URL')) do |conn|
      conn.request :json
      conn.response :json
      conn.adapter Faraday.default_adapter
    end
  end

  def get
    api_connection().get do |request|
      request.url "payment/#{@payment_id}"
    end
  end

  def get_by_order
    api_connection().get do |request|
      request.url "paymentOrder/#{@order_id}"
    end
  end

  def create(amount, status, installments, credit_card_id)
    api_connection().post do |request|
      request.url "payment"
      request.body = {
        user_id: @user_id,
        amount: amount,
        order_id: @order_id,
        status: status,
        installments: installments,
        credit_card_id: credit_card_id
      }
    end
  end

  def update(status)
    api_connection().put do |request|
      request.url "paymentStatus/#{@payment_id}"
      request.body = {
        status: status,
      }
    end
  end
end
