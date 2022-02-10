import { Component, OnDestroy, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ProjectArea } from 'src/models/projects/projectArea';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-project-areas',
  templateUrl: './project-areas.component.html',
  styleUrls: ['./project-areas.component.css']
})
export class ProjectAreasComponent implements OnInit {

  public projectAreas: ProjectArea[] = [
    new ProjectArea(1, "Первая", ""),
    new ProjectArea(2, "Вторая", ""),
    new ProjectArea(3, "Третья", ""),
    new ProjectArea(4, "Четвертая", "")
  ]

  public currentProjectArea?: ProjectArea;

  constructor(private sanitizer: DomSanitizer,
    public modalService: ModalService) { }

  ngOnInit(): void { }

  getUrlFromIcon(icon:string) {
    if(icon == "") return "assets/doge.jpg";
    return  this.sanitizer.bypassSecurityTrustResourceUrl(
        `data:image/png;base64, ${icon}`);
  }

  addProjectArea() {
    this.modalService.openModal('add-project-area');
  }

  editProjectArea(area: ProjectArea) {
    this.modalService.openModal('edit-project-area');
  }

  deleteProjectArea(area: ProjectArea) {
    this.projectAreas = this.projectAreas.filter(a => a !== area);
  }
}
