import { Body, Controller, Get, Post } from '@nestjs/common';
import { CreditCard as CreditCardModel } from '@prisma/client';
import { CreditCardService } from './creditCard.service';

@Controller('credit-card')
export class CreditCardController {
  constructor(private readonly creditCardService: CreditCardService) {}

  @Post('creditCard')
  async createCreditCard(
    @Body()
    creditCardData: {
      number: string;
      expiry: string;
      document: string;
      name: string;
      user_id: string;
    },
  ): Promise<CreditCardModel> {
    return this.creditCardService.createCreditCard(creditCardData);
  }
}
