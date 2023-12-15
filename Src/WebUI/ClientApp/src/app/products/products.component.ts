import { Component, inject, OnInit } from '@angular/core';
import { Client, ProductsListVm } from '../northwind-traders-api';
import { saveAs } from 'file-saver';

@Component({
  templateUrl: './products.component.html'
})
export class ProductsComponent implements OnInit {
  private client = inject(Client);

  protected productsListVm: ProductsListVm;

  ngOnInit(): void {
    this.client.getProductsList().subscribe(result => {
      this.productsListVm = result;
    });
  }

  protected exportAsCsv() {
    this.client.getProductsCsv().subscribe(result => {
      const blob = new Blob([result.data], { type: result.headers.contentType });
      saveAs(blob, result.fileName);
    });
  }
}
