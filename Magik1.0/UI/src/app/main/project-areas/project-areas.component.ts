import {Component, EventEmitter, HostListener, OnChanges, OnInit, Output} from '@angular/core';
import {ProjectArea} from "../../models/projectArea";
import {ProjectAreasService} from "../../services/project-areas/project-areas.service";
import {PagingService} from "../../services/tools/paging.service";
import {ProjectAreaIcon} from "../../models/icons/projectAreaIcon";

@Component({
  selector: 'app-project-areas',
  templateUrl: './project-areas.component.html',
  styleUrls: ['./project-areas.component.css']
})
export class ProjectAreasComponent implements OnInit {
  pagingService?: PagingService;

  currentProjectArea?: ProjectArea;
  projectAreas?: Array<ProjectArea>;

  projectAreaIcons?: Array<ProjectAreaIcon>;

  isEditProjectArea: boolean = false;
  isCreateProjectArea: boolean = false;

  createProjectAreaName?: string;
  selectedCreateProjectAreaIcon?: ProjectAreaIcon;

  @Output() currentProjectAreaChange = new EventEmitter<ProjectArea>();

  constructor(private paService: ProjectAreasService) {
  }

  @HostListener('window:resize', ['$event'])
  onWindowResize() {
    this.pagingService!.refresh();
  }

  ngOnInit(): void {
    this.getProjectAreas();
    this.getProjectAreaIcons();
  }

  selectProjectArea(area: ProjectArea) {
    if(area != this.currentProjectArea) {
      this.currentProjectArea = area;
      this.currentProjectAreaChange.emit(this.currentProjectArea);
      this.isCreateProjectArea = false;
      this.isEditProjectArea = false;
    }
  }

  getProjectAreaIcons() {
    this.paService.getProjectAreaIcons()
      .subscribe(res => {
        this.projectAreaIcons = res;
        this.selectedCreateProjectAreaIcon = this.projectAreaIcons![0]
      }, err => {
        console.log(err);
      })
  }

  getIconForArea(area: ProjectArea) {
    return this.projectAreaIcons!.filter(icon => icon.id == area.iconId)[0].base64Icon;
  }

  getProjectAreas() {
    this.paService.getProjectAreas().subscribe(res => {
      this.projectAreas = res;
      this.pagingService = new PagingService(this.projectAreas);
      if(this.projectAreas.length != 0) {
        this.currentProjectArea = this.projectAreas[0];
        this.currentProjectAreaChange.emit(this.currentProjectArea);
      }
    }, err => {
      console.log(err);
    });
  }

  createProjectArea() {
    let projectArea = new ProjectArea(0, this.createProjectAreaName!,
      this.selectedCreateProjectAreaIcon!.id);
    this.paService.createProjectArea(projectArea).subscribe(res => {
      this.projectAreas!.push(res);
      this.pagingService!.refresh();
      this.isCreateProjectArea = false;
    }, err => {
      console.log(err);
    });
  }

  editProjectArea(area: ProjectArea) {
    if(this.isEditProjectArea) {
      this.paService.editProjectArea(area)
        .subscribe(res => {

        }, err => {
          console.log(err);
        });
      this.isEditProjectArea = false;
    }
    else {
      this.isEditProjectArea = true;
    }
  }

  deleteProjectArea(area: ProjectArea) {
    if(confirm("Вы уверены что хотите удалить эту категорию проектов?")) {
      this.paService.deleteProjectArea(area)
        .subscribe(res => {
            this.getProjectAreas();
          },
          err => {
            console.log(err);
          });
    }
  }
}
