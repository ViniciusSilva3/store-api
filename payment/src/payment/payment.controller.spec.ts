import { Test, TestingModule } from '@nestjs/testing';
import { Prisma } from '@prisma/client';
import { PaymentController } from './payment.controller';
import { PaymentService } from './payment.service';

describe('PaymentController', () => {
  let paymentController: PaymentController;
  // let paymentService: PaymentService;

  const payment: Prisma.PaymentCreateInput = {
    amount: 20000,
    status: 'pending',
    order_id: '123456789',
    installments: 1,
    user_id: '123456789',
    // credit_card_id: '123456789',
  };

  beforeEach(async () => {
    const app: TestingModule = await Test.createTestingModule({
      controllers: [PaymentController],
      providers: [
        {
          provide: PaymentService,
          useValue: {
            createPayment: jest.fn(() => payment),
            payment: jest.fn(() => payment),
          },
        },
      ],
    }).compile();

    // paymentService = app.get<PaymentService>(PaymentService);
    paymentController = app.get<PaymentController>(PaymentController);
  });

  it('should be defined', () => {
    expect(paymentController).toBeDefined();
  });

  describe('createPayment', () => {
    it('should create a payment', async () => {
      const res = await paymentController.createPayment({
        amount: 20000,
        status: 'pending',
        order_id: '123456789',
        installments: 1,
        user_id: '123456789',
        credit_card_id: '123456789',
      });

      expect(res).toEqual(payment);
    });
  });

  describe('getPayment', () => {
    it('should get a payment', async () => {
      const res = await paymentController.getPayment(payment.id);

      expect(res).toEqual(payment);
    });
  });
});
