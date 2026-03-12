import { Component, signal } from '@angular/core';
import { SearchInputComponent } from '../../components/search-input/search-input.component';
import { CountryListComponent } from '../../components/country-list/country-list.component';
import { RESTCountry } from '../../interfaces/rest-countries.interface';

@Component({
  selector: 'app-by-country-page',
  templateUrl: './by-country-page.component.html',
  imports: [SearchInputComponent, CountryListComponent],
})
export class ByCountryPageComponent {
  countries = signal<RESTCountry[]>([]);

  onSearch(value: string) {
    console.log({ value });
  }
}
