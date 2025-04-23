export class SystemSetting {
    private name: string;
    private description: string;
    private value: string;
    private dataType: string;
    private isRequred: boolean;
    private isReadOnly: boolean;

    constructor(name: string, description: string, value: string, dataType: string, isRequired: boolean, isReadOnly: boolean) {
        this.name = name;
        this.description = description;
        this.value = value;
        this.dataType = dataType;
        this.isRequred = isRequired;
        this.isReadOnly = isReadOnly;
    }

    setValue(name:string, value:string) {
        
    }
}