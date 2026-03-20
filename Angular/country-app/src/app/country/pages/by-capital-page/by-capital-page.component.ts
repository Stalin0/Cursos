import { Component, inject, resource, signal } from '@angular/core';
import { SearchInputComponent } from '../../components/search-input/search-input.component';
import { CountryListComponent } from '../../components/country-list/country-list.component';
import { CountryService } from '../../services/country.service';
import { rxResource } from '@angular/core/rxjs-interop';
import { of } from 'rxjs';
@Component({
  selector: 'app-by-capital-page',
  templateUrl: './by-capital-page.component.html',
  imports: [SearchInputComponent, CountryListComponent],
})
export class ByCapitalPageComponent {
  countryServices = inject(CountryService);
  query = signal('');

  countryResource = rxResource({
    params: () => ({ query: this.query() }),
    stream: ({ params }) => {
      console.log(params);
      if (!params.query) return of([]);

      return this.countryServices.searchByCapital(params.query);
    },
  });
  // countryResource = resource({
  //   params: () => ({ query: this.query() }),
  //   loader: async ({ params }) => {
  //     if (!params.query) return [];
  //     return await firstValueFrom(this.countryServices.searchByCapital(params.query));
  //   },
  // });

  // isLoading = signal(false);
  // isError = signal<string | null>(null);
  // countries = signal<Country[]>([]);

  // onSearch(query: string) {
  //   if (this.isLoading()) return;

  //   this.isLoading.set(true);
  //   this.isError.set(null);

  //   this.countryServices.searchByCapital(query).subscribe({
  //     next: (countries) => {
  //       this.isLoading.set(false);
  //       this.countries.set(countries);
  //     },
  //     error: (error) => {
  //       this.isLoading.set(false);
  //       this.countries.set([]);
  //       this.isError.set(error);
  //     },
  //   });
  // }
}
