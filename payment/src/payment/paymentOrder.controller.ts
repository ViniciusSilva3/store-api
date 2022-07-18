import { Controller, Get, Param } from '@nestjs/common';
import { Payment as PaymentModel } from '@prisma/client';
import { PaymentService } from './payment.service';

@Controller('paymentOrder')
export class PaymentOrderController {
  constructor(private readonly paymentService: PaymentService) {}

  @Get('/:order_id')
  async getPaymentByOrderId(
    @Param('order_id') order_id: string,
  ): Promise<PaymentModel[]> {
    return this.paymentService.payments({ order_id });
  }
}
