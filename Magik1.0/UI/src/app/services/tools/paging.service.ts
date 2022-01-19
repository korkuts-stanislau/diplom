import { Injectable } from '@angular/core';
import {ProjectArea} from "../../models/projectArea";

@Injectable({
  providedIn: 'root'
})
export class PagingService {
  public currentPageElements?: Array<any>;
  public pagesCount?: number;
  public elementsByPageCount?: number;
  public currentPage: number = 1;

  public hasNextPage?: boolean = false;
  public hasPreviousPage?: boolean = false

  constructor(private elements: Array<any>) {
    this.refresh();
  }

  public refresh() {
    if(this.elements.length == 0) {
      return;
    }
    this.elementsByPageCount = Math.ceil(window.innerWidth / 300);
    this.pagesCount = this.elements.length / this.elementsByPageCount;

    this.calculateCurrentPageElements();
    this.checkPageProperties();

    while(this.currentPageElements?.length == 0) {
      this.previousPage();
    }
  }

  private calculateCurrentPageElements() {
    let skipElementsCount = (this.currentPage! - 1) * this.elementsByPageCount!;
    this.currentPageElements = this.elements
      .filter((u, i) => i >= skipElementsCount)
      .filter((u, i) => i < this.elementsByPageCount!);
  }

  private checkPageProperties() {
    this.hasNextPage = this.currentPage < this.pagesCount!;
    this.hasPreviousPage = this.currentPage > 1;
  }

  public nextPage() {
    if(this.hasNextPage) {
      this.currentPage++;
      this.checkPageProperties();
      this.calculateCurrentPageElements();
    }
  }

  public previousPage() {
    if(this.hasPreviousPage) {
      this.currentPage--;
      this.checkPageProperties();
      this.calculateCurrentPageElements();
    }
  }
}
