import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PicturesComponent } from './pictures.component';
import { PicturesRoutingModule } from './pictures-routing.module';



@NgModule({
  declarations: [PicturesComponent],
  imports: [
    CommonModule,
    PicturesRoutingModule
  ]
})
export class PicturesModule { }
