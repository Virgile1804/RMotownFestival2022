import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PicturesApiService } from 'src/app/api-services/pictures-api.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html'
})
export class UploadComponent implements OnInit, OnDestroy {
  private readonly destroy$ = new Subject();

  uploadForm: FormGroup;
  private fileToUpload: File;

  constructor(private picturesApiService: PicturesApiService) { }

  ngOnInit(): void {
    this.uploadForm = new FormGroup({
      file: new FormControl(undefined, Validators.required)
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
  }

  onFileChange(event: InputEvent): void {
    const target = event.target as HTMLInputElement;
    if (!target.files || !target.files.length) {
      this.fileToUpload = null;
    }

    this.fileToUpload = target.files[0];
  }

  onFormSubmit(): void {
    if (!this.uploadForm.valid) {
      return;
    }

    this.picturesApiService.upload(this.fileToUpload)
      .pipe(takeUntil(this.destroy$))
      .subscribe();
  }

}
