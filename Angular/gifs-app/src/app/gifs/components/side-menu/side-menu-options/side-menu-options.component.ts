import { Component, OnInit } from "@angular/core";
import { RouterLink, RouterLinkActive } from "@angular/router";

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
  imports: [RouterLink],
})
export default class SideMenuOptionsComponent {
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
