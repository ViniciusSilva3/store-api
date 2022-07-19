import { IsInt, IsString, IsArray } from 'class-validator';

import { ProductDto } from '../product/product.dto';

export class OrderDto {
  @IsString()
  readonly id: string;

  @IsString()
  readonly userId: string;

  @IsInt()
  readonly cost: number;

  @IsString()
  readonly status: string;

  @IsString()
  readonly createdAt: string;

  @IsString()
  readonly updatedAt: string;

  @IsArray()
  readonly products: ProductDto[];
}
