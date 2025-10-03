import { ChangeDetectionStrategy, Component, inject, OnInit } from "@angular/core";
import { RouterLink, RouterLinkActive } from "@angular/router";
import { GifService } from "@gifs/services/gifs.service";

interface MenuOption {
  label: string;
  subLabel: string;
  icon: string;
  route: string;
}

@Component({
  selector: "gifs-side-menu-options",
  templateUrl: "./side-menu-options.component.html",
  styleUrls: [],
  changeDetection: ChangeDetectionStrategy.OnPush, //UI mas fluida al User
  imports: [RouterLink],
})
export default class SideMenuOptionsComponent {
  private readonly gifService = inject(GifService)
  readonly searchHistoryKeys = this.gifService.searchHistoryKeys
  menuOptions: MenuOption[] = [
    {
      label: "Trending",
      subLabel: "Gifs populares",
      icon: "fa-solid fa-chart-line",
      route: "/dashboard/trending",
    },
    {
      label: "Buscador",
      subLabel: "Buscar gifs",
      icon: "fa-solid fa-magnifying-glass",
      route: "/dashboard/search",
    },
  ];
}
