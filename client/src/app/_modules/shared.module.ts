import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(), // using ngx-bootstrap support for UI
    ToastrModule.forRoot({
      timeOut: 1500,
    }),
  ],
  exports: [BsDropdownModule, ToastrModule],
})
export class SharedModule {}
