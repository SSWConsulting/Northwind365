import { Component, inject, OnInit } from '@angular/core';
import { Client, CustomersListVm } from '../northwind-traders-api';
import { CustomerDetailComponent } from '../customer-detail/customer-detail.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html'
})
export class CustomersComponent implements OnInit {
  private client = inject(Client);
  private modalService =inject(BsModalService);

  protected vm: CustomersListVm;

  ngOnInit(): void {
    this.client.getCustomersList().subscribe(result => {
      this.vm = result;
    });
  }

  public customerDetail(id: string) {
    this.client.getCustomer(id).subscribe(result => {
      const initialState = {
        customer: result
      };
      this.modalService.show(CustomerDetailComponent, {initialState});
    });
  }

  protected exportAsCsv() {
    this.client.getCustomersCsv().subscribe(result => {
      const blob = new Blob([result.data], { type: result.headers.contentType });
      saveAs(blob, result.fileName);
    });
  }
}
