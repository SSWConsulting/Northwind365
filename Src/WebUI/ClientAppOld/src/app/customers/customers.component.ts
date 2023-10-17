import { Component } from '@angular/core';
import { Client, CustomersListVm } from '../northwind-traders-api';
import { CustomerDetailComponent } from '../customer-detail/customer-detail.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html'
})
export class CustomersComponent {

  public vm: CustomersListVm = new CustomersListVm();
  private bsModalRef: BsModalRef;

  constructor(private client: Client, private modalService: BsModalService) {
    client.getCustomersList().subscribe(result => {
      this.vm = result;
    }, error => console.error(error));
  }

  public customerDetail(id: string) {
    this.client.getCustomer(id).subscribe(result => {
      const initialState = {
        customer: result
      };
      this.bsModalRef = this.modalService.show(CustomerDetailComponent, {initialState});
    }, error => console.error(error));
  }
}
