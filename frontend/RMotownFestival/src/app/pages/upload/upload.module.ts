import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UploadComponent } from './upload.component';
import { UploadRoutingModule } from './upload-routing.module';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [UploadComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,

    UploadRoutingModule,
  ]
})
export class UploadModule { }
