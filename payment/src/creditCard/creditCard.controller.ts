import {
  Body,
  Controller,
  Delete,
  Get,
  Post,
  Param,
  Patch,
} from '@nestjs/common';
import { CreditCard as CreditCardModel } from '@prisma/client';
import { CreditCardService } from './creditCard.service';

@Controller('creditCard')
export class CreditCardController {
  constructor(private readonly creditCardService: CreditCardService) {}

  @Get('/:id')
  async getCreditCard(@Param('id') id: string): Promise<CreditCardModel> {
    return this.creditCardService.creditCard({ id });
  }

  @Get('user/:user_id')
  async getCreditCards(
    @Param('user_id') user_id: string,
  ): Promise<CreditCardModel[]> {
    return this.creditCardService.getUserCreditCards(user_id);
  }

  @Post()
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

  @Delete('/:id')
  async deleteCreditCard(@Param('id') id: string): Promise<CreditCardModel> {
    return this.creditCardService.deleteCreditCard({ id });
  }

  @Patch('/:id')
  async updateCreditCard(
    @Param('id') id: string,
    @Body()
    creditCardData: {
      number?: string;
      expiry?: string;
      document?: string;
      name?: string;
      user_id?: string;
    },
  ): Promise<CreditCardModel> {
    return this.creditCardService.updateCreditCard({
      where: { id },
      data: creditCardData,
    });
  }
}
