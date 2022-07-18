import { Body, Controller, Patch, Param } from '@nestjs/common';
import { Payment as PaymentModel } from '@prisma/client';
import { PaymentService } from './payment.service';

@Controller('paymentStatus')
export class PaymentStatusController {
  constructor(private readonly paymentService: PaymentService) {}

  @Patch('/:id')
  async createPayment(
    @Param('id') id: string,
    @Body()
    paymentData: {
      status: string;
    },
  ): Promise<PaymentModel> {
    return this.paymentService.updatePayment({
      data: paymentData,
      where: { id },
    });
  }
}
