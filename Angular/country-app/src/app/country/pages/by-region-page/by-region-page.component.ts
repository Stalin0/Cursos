import { Component, signal } from '@angular/core';
import { CountryListComponent } from '../../components/country-list/country-list.component';
import { RESTCountry } from '../../interfaces/rest-countries.interface';

@Component({
  selector: 'app-by-region-page',
  templateUrl: './by-region-page.component.html',
  imports: [CountryListComponent],
})
export class ByRegionPageComponent {
  countries = signal<RESTCountry[]>([]);
}
