import { Component, computed, inject } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { map } from "rxjs";
import { toSignal } from "@angular/core/rxjs-interop";
import { GifService } from "@gifs/services/gifs.service";
import { GifListComponent } from "@gifs/components/gif-list/gif-list.component";

@Component({
  selector: "app-gif-history",
  templateUrl: "./gif-history.component.html",
  styleUrls: [],
  imports: [GifListComponent],
})
export default class GifHistoryComponent {
  gifService = inject(GifService);
  query = toSignal(inject(ActivatedRoute).params.pipe(map((params) => params["query"])));

  gifsByKey = computed(() => this.gifService.getHistoryGifs(this.query()));
}
