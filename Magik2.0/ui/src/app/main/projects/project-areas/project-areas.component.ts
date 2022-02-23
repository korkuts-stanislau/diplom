import { Component, OnDestroy, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ProjectArea } from 'src/models/projects/projectArea';
import { ModalService } from 'src/services/modal/modal.service';
import { ProjectAreaService } from 'src/services/project/project-area/project-area.service';

@Component({
  selector: 'app-project-areas',
  templateUrl: './project-areas.component.html',
  styleUrls: ['./project-areas.component.css']
})
export class ProjectAreasComponent implements OnInit {

  public projectAreas?:ProjectArea[];

  public currentProjectArea?: ProjectArea;

  constructor(private sanitizer: DomSanitizer,
    public modalService: ModalService,
    private projectAreaService: ProjectAreaService) { }

  ngOnInit(): void {
    this.getProjectAreas();
  }

  getUrlFromIcon(icon:string) {
    if(icon == "") return "assets/puzzle.jpg";
    return  this.sanitizer.bypassSecurityTrustResourceUrl(
        `data:image/png;base64, ${icon}`);
  }

  getProjectAreas() {
    this.projectAreaService.getProjectAreas()
      .subscribe(res => {
        this.projectAreas = res;
      },err => {
        alert("Не удалось получить данные с сервера");
        console.log(err);
      });
  }

  addProjectAreaModal() {
    this.modalService.openModal('add-project-area');
  }

  addProjectArea(area: ProjectArea) {
    this.projectAreaService.addProjectArea(area)
      .subscribe(res => {
        area.id = res;
        this.projectAreas!.push(area);
      }, err => {
        console.log(err);
        alert("Добавление не удалось");
      });
  }

  editProjectAreaModal() {
    this.modalService.openModal('edit-project-area');
  }

  editProjectArea(area: ProjectArea) {
    this.projectAreaService.editProjectArea(area)
      .subscribe(res => {
        
      }, err => {
        console.log(err);
        alert("Изменение не удалось");
      });
  }

  deleteProjectArea(area: ProjectArea) {
    if(confirm("Вы уверены что хотите удалить эту область проектов?")) {
      this.projectAreaService.deleteProjectArea(area)
        .subscribe(res => {
          this.projectAreas = this.projectAreas?.filter(a => a !== area);
        }, err => {
          console.log(err);
          alert("Удаление не удалось");
        });
    }
  }
}
