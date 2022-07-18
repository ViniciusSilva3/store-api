import { Body, Controller, Get, Post, Param } from '@nestjs/common';
import { Payment as PaymentModel } from '@prisma/client';
import { PaymentService } from './payment.service';

@Controller('payment')
export class PaymentController {
  constructor(private readonly paymentService: PaymentService) {}

  @Post()
  async createPayment(
    @Body()
    paymentData: {
      amount: number;
      user_id: string;
      status: string;
      order_id: string;
      credit_card_id: string;
    },
  ): Promise<PaymentModel> {
    return this.paymentService.createPayment(paymentData);
  }

  @Get('/:id')
  async getPayment(@Param('id') id: string): Promise<PaymentModel> {
    return this.paymentService.payment({ id });
  }
}
