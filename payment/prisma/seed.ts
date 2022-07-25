import { PrismaClient, Prisma } from '@prisma/client';

const prisma = new PrismaClient();

const creditCard: Prisma.CreditCardCreateInput[] = [
  {
    number: '4111111111111111',
    expiry: '22/04',
    document: '03332314068',
    name: 'Card holder name',
    user_id: '8f184cc1-7da8-4556-8e5f-fbf68727aa3d',
  },
];

const load = async () => {
  await prisma.creditCard.createMany({
    data: creditCard,
  });
  console.log('Database loaded with credit cards');
};

load()
  .then(async () => await prisma.$disconnect())
  .catch(async (e) => {
    console.log(e);
    await prisma.$disconnect();
    process.exit(1);
  });
