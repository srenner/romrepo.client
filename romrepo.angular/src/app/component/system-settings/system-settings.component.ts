import { Component, inject, Injectable, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';
import { SystemSetting } from '../../interface/system-setting';
import { Subscriber } from 'rxjs';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-system-settings',
  standalone: false,
  templateUrl: './system-settings.component.html',
  styleUrl: './system-settings.component.css'
})
export class SystemSettingsComponent implements OnInit {

  private apiService = inject(ApiService);
  settings:SystemSetting[] = [];
  
  constructor() {}

  ngOnInit(): void {
    this.getSettings();
  }

  getSettings() {
    this.apiService.getSystemSettings().subscribe(response => {
      this.settings = response || [];
    });
  }

}
