import { Test, TestingModule } from '@nestjs/testing';
import { Prisma } from '@prisma/client';
import { CreditCardController } from './creditCard.controller';
import { CreditCardService } from './creditCard.service';

describe('CreditCardController', () => {
  let creditCardController: CreditCardController;
  let creditCardService: CreditCardService;

  const creditCard: Prisma.CreditCardCreateInput = {
    number: '4111111111111111',
    expiry: '12/20',
    document: '123456789',
    name: 'John Doe',
    user_id: '123456789',
  };

  const creditCardUpdate: Prisma.CreditCardCreateInput = {
    number: '4111111111111111',
    expiry: '12/20',
    document: '123456789',
    name: 'John Doe Updated',
    user_id: '123456789',
  };

  beforeEach(async () => {
    const app: TestingModule = await Test.createTestingModule({
      controllers: [CreditCardController],
      providers: [
        {
          provide: CreditCardService,
          useValue: {
            createCreditCard: jest.fn(() => creditCard),
            getUserCreditCards: jest.fn(() => [creditCard]),
            creditCard: jest.fn(() => creditCard),
            deleteCreditCard: jest.fn(() => null),
            updateCreditCard: jest.fn(() => creditCardUpdate),
          },
        },
      ],
    }).compile();

    creditCardService = app.get<CreditCardService>(CreditCardService);
    creditCardController = app.get<CreditCardController>(CreditCardController);
  });

  it('should be defined', () => {
    expect(creditCardController).toBeDefined();
  });

  describe('createCreditCard', () => {
    it('should create a credit card', async () => {
      const res = await creditCardController.createCreditCard(creditCard);

      expect(res).toEqual(creditCard);
    });
  });

  describe('getUserCreditCards', () => {
    it('should get all credit cards of a user', async () => {
      const res = await creditCardController.getCreditCards(creditCard.user_id);

      expect(res).toEqual([creditCard]);
    });
  });

  describe('getCreditCard', () => {
    it('should get a credit card', async () => {
      const res = await creditCardController.getCreditCard(creditCard.id);

      expect(res).toEqual(creditCard);
    });
  });

  describe('deleteCreditCard', () => {
    it('should delete a credit card', async () => {
      const res = await creditCardController.deleteCreditCard(creditCard.id);

      expect(res).toEqual(null);
    });
  });

  describe('updateCreditCard', () => {
    it('should update a credit card', async () => {
      const res = await creditCardController.updateCreditCard(
        creditCard.id,
        creditCardUpdate,
      );

      expect(res).toEqual(creditCardUpdate);
    });
  });
});
