import { Component } from '@angular/core';
import { Client, ProductsListVm } from '../northwind-traders-api';

@Component({
  templateUrl: './products.component.html'
})
export class ProductsComponent {
  protected productsListVm: ProductsListVm;

  constructor(client: Client) {
    client.getProductsList().subscribe(result => {
      this.productsListVm = result;
    }, error => console.error(error));
  }
}
