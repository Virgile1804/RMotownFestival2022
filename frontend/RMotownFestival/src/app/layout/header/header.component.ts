import { Component, OnInit } from '@angular/core';
import { AppSettingsApiService } from 'src/app/api-services/appsettings-api.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit {
  festivalName = environment.festivalName;

  constructor(private appSettingsApiService: AppSettingsApiService) {}

  ngOnInit(): void {
    this.appSettingsApiService
      .getSettings()
      .subscribe(
        (appsettings) =>
          (this.festivalName =
            appsettings.festivalName ?? environment.festivalName)
      );
  }
}
