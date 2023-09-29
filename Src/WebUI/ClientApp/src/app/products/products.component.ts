import { Component } from '@angular/core';
import { Client, ProductsListVm } from '../northwind-traders-api';

@Component({
  templateUrl: './products.component.html'
})
export class ProductsComponent {

  productsListVm: ProductsListVm = new ProductsListVm();

  constructor(client: Client) {
    client.getProductsList().subscribe(result => {
      this.productsListVm = result;
    }, error => console.error(error));
  }
}
