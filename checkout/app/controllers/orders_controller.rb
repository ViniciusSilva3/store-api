class OrdersController < ApplicationController

  def create
    begin
      cart = get_user_cart(params[:user_id])
      cart_amount_value = get_cart_amount_value(cart)
      shipping_value = get_shipping_value(cart)
      total_value = cart_amount_value + shipping_value

      #order creation
      order = Order.new(user_id: params[:user_id], cart: cart, shipping_value: shipping_value, payment_amount: total_value)
      raise StandardError.new "Error when trying to create order" unless order.save

      #payment creation
      payment = create_payment(user_id, order_id, amount_value, nil, nil, params[:credit_card_id])

      #update order with payment info
      order.update(payment_id: payment['id'])

      render json: { order: order.to_json }, status: :created
    rescue => e
      render json: { error: e.message }, status: :unprocessable_entity
    end
  end

  private

  def get_user_cart(user_id)
    CartService.new(user_id).get()
  end

  def get_cart_amount_value(cart)
    amount = 0
    cart.each do |product|
      product_catalog = CatalogService.get(product.product_id)
      amount += product_catalog['price'].to_i * product['quantity']
    end
    amount
  end

  def get_shipping_value(cart)
    product_list = { product_list: cart }
    shipping = ShippingService.new(product_list).create()
    shipping['value']
  end

  def create_payment(user_id, order_id, amount, status, installments, credit_card_id)
    PaymentService.new(user_id: user_id, order_id: order_id).create(amount, status, installments, credit_card_id)
  end
end
