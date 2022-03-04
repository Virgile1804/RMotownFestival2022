import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PicturesApiService } from 'src/app/api-services/pictures-api.service';
import { share } from 'rxjs/operators';

@Component({
  selector: 'app-pictures',
  templateUrl: './pictures.component.html'
})
export class PicturesComponent implements OnInit {

  pictureUrls$: Observable<string[]>;

  constructor(private picturesApiService: PicturesApiService) { }

  ngOnInit(): void {
    this.pictureUrls$ = this.picturesApiService.getAllUrls()
      .pipe(share());
  }
}
