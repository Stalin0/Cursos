import { AfterViewInit, Component, ElementRef, inject, viewChild } from "@angular/core";
import { GifService } from "../../services/gifs.service";
import { ScrollStateService } from "@shared/services/scroll-state.service";

@Component({
  selector: "app-trending-page",
  templateUrl: "./trending-page.component.html",
})
export default class TrendingPageComponent implements AfterViewInit {
  gifService = inject(GifService);
  scrollStateService = inject(ScrollStateService);

  scrollDivRef = viewChild<ElementRef<HTMLDivElement>>("groupDiv");

  ngAfterViewInit(): void {
    const scrollDiv = this.scrollDivRef()?.nativeElement;
    if (!scrollDiv) return;

    scrollDiv.scrollTop = this.scrollStateService.trendingScrollState();
  }

  onScroll(event: Event) {
    const scrollDiv = this.scrollDivRef()?.nativeElement;
    if (!scrollDiv) return;
    const scrollTop = scrollDiv.scrollTop;
    const clientHeignt = scrollDiv.clientHeight;
    const scrollHeight = scrollDiv.scrollHeight;
    // console.log({ scrollTop: scrollHeight + scrollTop, clientHeignt });
    const isAtBottom = scrollTop + clientHeignt + 300 >= scrollHeight;

    this.scrollStateService.trendingScrollState.set(scrollTop);
    if (isAtBottom) {
      this.gifService.loadTrendingGifs();
    }
  }
}
