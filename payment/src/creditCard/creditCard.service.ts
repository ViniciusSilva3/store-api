import { Injectable } from '@nestjs/common';
import { PrismaService } from '../prisma.service';
import { CreditCard, Prisma } from '@prisma/client';

@Injectable()
export class CreditCardService {
  constructor(private prisma: PrismaService) {}

  async creditCard(
    creditCardWhereUniqueInput: Prisma.CreditCardWhereUniqueInput,
  ): Promise<CreditCard | null> {
    return this.prisma.creditCard.findUnique({
      where: creditCardWhereUniqueInput,
    });
  }

  async creditCards(params: {
    skip?: number;
    take?: number;
    cursor?: Prisma.CreditCardWhereUniqueInput;
    where?: Prisma.CreditCardWhereInput;
    orderBy?: Prisma.CreditCardOrderByWithRelationInput;
  }): Promise<CreditCard[]> {
    const { skip, take, cursor, where, orderBy } = params;
    return this.prisma.creditCard.findMany({
      skip,
      take,
      cursor,
      where,
      orderBy,
    });
  }

  async createCreditCard(
    data: Prisma.CreditCardCreateInput,
  ): Promise<CreditCard> {
    return this.prisma.creditCard.create({
      data,
    });
  }

  async updateCreditCard(params: {
    where: Prisma.CreditCardWhereUniqueInput;
    data: Prisma.CreditCardUpdateInput;
  }): Promise<CreditCard> {
    const { where, data } = params;
    return this.prisma.creditCard.update({
      data,
      where,
    });
  }

  async deleteCreditCard(
    where: Prisma.CreditCardWhereUniqueInput,
  ): Promise<CreditCard> {
    return this.prisma.creditCard.delete({
      where,
    });
  }
}
