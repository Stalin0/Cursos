export interface RESTCountry {
  tld: string[];
  cca2: string;
  ccn3: string;
  cca3: string;
  cioc: string;
  independent: boolean;
  status: string;
  unMember: boolean;
  idd: Idd;
  capital: string[];
  altSpellings: string[];
  region: string;
  subregion: string;
  landlocked: boolean;
  borders: string[];
  area: number;
  maps: Maps;
  population: number;
  fifa: string;
  car: Car;
  timezones: string[];
  continents: string[];
  flag: string;
  name: Name;
  currencies: Currencies;
  languages: Languages;
  latlng: number[];
  demonyms: Demonyms;
  translations: Translations;
  gini: Gini;
  flags: Flags;
  coatOfArms: CoatOfArms;
  startOfWeek: string;
  capitalInfo: CapitalInfo;
  postalCode: PostalCode;
}

export interface Idd {
  root: string;
  suffixes: string[];
}

export interface Maps {
  googleMaps: string;
  openStreetMaps: string;
}

export interface Car {
  signs: string[];
  side: string;
}

export interface Name {
  common: string;
  official: string;
  nativeName: NativeName;
}

export interface NativeName {
  spa: TranslationValue;
}

export interface TranslationValue {
  official: string;
  common: string;
}

export interface Currencies {
  USD: CurrencyDetail;
}

export interface CurrencyDetail {
  symbol: string;
  name: string;
}

export interface Languages {
  spa: string;
}

export interface Demonyms {
  eng: GenderName;
  fra: GenderName;
}

export interface GenderName {
  f: string;
  m: string;
}

export interface Translations {
  ara: TranslationValue;
  bre: TranslationValue;
  ces: TranslationValue;
  cym: TranslationValue;
  deu: TranslationValue;
  est: TranslationValue;
  fin: TranslationValue;
  fra: TranslationValue;
  hrv: TranslationValue;
  hun: TranslationValue;
  ind: TranslationValue;
  ita: TranslationValue;
  jpn: TranslationValue;
  kor: TranslationValue;
  nld: TranslationValue;
  per: TranslationValue;
  pol: TranslationValue;
  por: TranslationValue;
  rus: TranslationValue;
  slk: TranslationValue;
  spa: TranslationValue;
  srp: TranslationValue;
  swe: TranslationValue;
  tur: TranslationValue;
  urd: TranslationValue;
  zho: TranslationValue;
}

export interface Gini {
  '2019': number;
}

export interface Flags {
  png: string;
  svg: string;
  alt: string;
}

export interface CoatOfArms {
  png: string;
  svg: string;
}

export interface CapitalInfo {
  latlng: number[];
}

export interface PostalCode {
  format: string;
  regex: string;
}
