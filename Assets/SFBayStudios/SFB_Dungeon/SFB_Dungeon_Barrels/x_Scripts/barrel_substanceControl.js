#pragma strict

var designOptions			: ProceduralMaterial[];
var mainObjects				: GameObject[];
var usedDesign				: int							= 0;

var subPanels				: GameObject[];
var object					: GameObject;
var rend					: Renderer;
var substance				: ProceduralMaterial;
var loadingPanel			: GameObject;

var groundDirtColor			: GameObject[];
var ceilingDirtColor		: GameObject[];
var dirtColor				: GameObject[];

var woodCount				: int				= 18;
var metalCount				: int				= 8;
var woodNumber				: int				= 1;
var metalNumber				: int				= 1;
var losePlanks				: int				= 10;
var switchDesign			: boolean			= true;

var panels					: GameObject[];

function Start() {
	designOptions[0].SetProceduralBoolean("UseWoodWeathering", true);
	designOptions[0].SetProceduralBoolean("UseMetalWear", true);
	loadingPanel.SetActive(true);
	if (!IsInvoking("CheckLoading"))
		InvokeRepeating("CheckLoading", 0, 0.2);
	ResetRenderer();
	ResetAll();
	
}

function ResetRenderer(){
	rend 					= object.GetComponent.<Renderer>();
	substance 				= rend.sharedMaterial as ProceduralMaterial;
}

function ResetAll(){
	
	RebuildTextures();
}

function LoadSubPanel(panelNumber : int){
	for (var i : int; i < subPanels.Length; i++){
		subPanels[i].SetActive(false);
	}
	subPanels[panelNumber].SetActive(true);
}

function CloseSubPanels(){
	for (var i : int; i < subPanels.Length; i++){
		subPanels[i].SetActive(false);
	}
}

function CheckLoading(){
	//print ("CheckLoading()");
	if (!designOptions[0].isProcessing && !designOptions[1].isProcessing)
	{
		loadingPanel.SetActive(false);
		CancelInvoke("CheckLoading");
	}
}

function SwitchDesign(){
	switchDesign = !switchDesign;
	designOptions[0].SetProceduralBoolean("MainSwitchDesign", switchDesign);
	designOptions[1].SetProceduralBoolean("MainSwitchDesign", switchDesign);
	RebuildTextures();
}

function NextWood(){
	woodNumber += 1;
	if (woodNumber > woodCount)
		woodNumber = 1;
	// Lose Planks
	if (woodNumber >= losePlanks + 1 || woodNumber <= 1)
		designOptions[1].SetProceduralBoolean("WoodUseSlats", false);
	else
		designOptions[1].SetProceduralBoolean("WoodUseSlats", true);
	designOptions[1].SetProceduralFloat("WoodMaterialNumber", woodNumber);
	RebuildTextures();
}

function PrevWood(){
	woodNumber -= 1;
	if (woodNumber == 0)
		woodNumber = woodCount;
	// Give Planks
	if (woodNumber >= losePlanks + 1 || woodNumber <= 1)
		designOptions[1].SetProceduralBoolean("WoodUseSlats", false);
	else
		designOptions[1].SetProceduralBoolean("WoodUseSlats", true);
	designOptions[1].SetProceduralFloat("WoodMaterialNumber", woodNumber);
	RebuildTextures();
}

function NextMetal(){
	metalNumber += 1;
	if (metalNumber > metalCount)
		metalNumber = 1;
	designOptions[1].SetProceduralFloat("MetalMaterialNumber", metalNumber);
	RebuildTextures();
}

function PrevMetal(){
	metalNumber -= 1;
	if (metalNumber == 0)
		metalNumber = metalCount;
	designOptions[1].SetProceduralFloat("MetalMaterialNumber", metalNumber);
	RebuildTextures();
}

function RebuildTextures(){
	//print ("RebuildTextures(), usedDesign: " + usedDesign);
	designOptions[0].RebuildTextures();
	designOptions[1].RebuildTextures();
	loadingPanel.SetActive(true);
	if (!IsInvoking("CheckLoading"))
		InvokeRepeating("CheckLoading", 0, 0.1);
}

function Update(){
	if (designOptions[usedDesign].isProcessing && !loadingPanel.activeSelf)
	{
		loadingPanel.SetActive(true);
		if (!IsInvoking("CheckLoading"))
			InvokeRepeating("CheckLoading", 0, 0.1);
	}
}

// DESIGN
function SetDesign(newDesign : int){
	usedDesign		= newDesign;
	panels[0].SetActive(false);
	panels[1].SetActive(false);
	panels[newDesign].SetActive(true);
	for (var i : int; i < mainObjects.Length; i++){
		mainObjects[i].GetComponent.<Renderer>().material = designOptions[newDesign];
	}
	RebuildTextures();
}

// BASE MATERIALS
function LoadWood(newMaterial : int){
	print ("LoadWood(" + newMaterial + ")");
	designOptions[0].SetProceduralFloat("WoodMaterialNumber", newMaterial);
	designOptions[1].SetProceduralFloat("WoodMaterialNumber", newMaterial);
	if (newMaterial == 2)
	{
		designOptions[0].SetProceduralBoolean("WoodRotate", false);
		designOptions[1].SetProceduralBoolean("WoodRotate", false);
	}
	else
	{
		designOptions[0].SetProceduralBoolean("WoodRotate", true);
		designOptions[1].SetProceduralBoolean("WoodRotate", true);
	}
	RebuildTextures();
}

function LoadMetal(newMaterial : int){
	designOptions[0].SetProceduralFloat("MetalMaterialNumber", newMaterial);
	designOptions[1].SetProceduralFloat("MetalMaterialNumber", newMaterial);
	RebuildTextures();
}

// WOOD WEATHERING
function WeatheringDust(newValue : float){
	designOptions[0].SetProceduralFloat("WoodWeatheringDust", newValue);
	designOptions[1].SetProceduralFloat("WoodWeatheringDust", newValue);
	RebuildTextures();
}

function WeatheringDirtiness(newValue : float){
	designOptions[0].SetProceduralFloat("WoodWeatheringDirtiness", newValue);
	designOptions[1].SetProceduralFloat("WoodWeatheringDirtiness", newValue);
	RebuildTextures();
}

function WeatheringEdgeWearing(newValue : float){
	designOptions[0].SetProceduralFloat("WoodWeatheringEdgeWearing", newValue);
	designOptions[1].SetProceduralFloat("WoodWeatheringEdgeWearing", newValue);
	RebuildTextures();
}

function WeatheringVarnishPeeling(newValue : float){
	designOptions[0].SetProceduralFloat("WoodWeatheringVarnishPeeling", newValue);
	designOptions[1].SetProceduralFloat("WoodWeatheringVarnishPeeling", newValue);
	RebuildTextures();
}

function WeatheringAge(newValue : float){
	designOptions[0].SetProceduralFloat("WoodWeatheringAge", newValue);
	designOptions[1].SetProceduralFloat("WoodWeatheringAge", newValue);
	RebuildTextures();
}

function WeatheringDesaturation(newValue : float){
	designOptions[0].SetProceduralFloat("WoodWeatheringDesaturation", newValue);
	designOptions[1].SetProceduralFloat("WoodWeatheringDesaturation", newValue);
	RebuildTextures();
}

function WeatheringBrightness(newValue : float){
	designOptions[0].SetProceduralFloat("WoodWeatheringBrightness", newValue);
	designOptions[1].SetProceduralFloat("WoodWeatheringBrightness", newValue);
	RebuildTextures();
}

// METAL WEAR
function MetalWearDust(newValue : float){
	designOptions[0].SetProceduralFloat("MetalWearDust", newValue);
	designOptions[1].SetProceduralFloat("MetalWearDust", newValue);
	RebuildTextures();
}

function MetalWearDirtiness(newValue : float){
	designOptions[0].SetProceduralFloat("MetalWearDirtiness", newValue);
	designOptions[1].SetProceduralFloat("MetalWearDirtiness", newValue);
	RebuildTextures();
}

function MetalWearEdgeWearing(newValue : float){
	designOptions[0].SetProceduralFloat("MetalWearEdgeWearing", newValue);
	designOptions[1].SetProceduralFloat("MetalWearEdgeWearing", newValue);
	RebuildTextures();
}

function MetalWearRust(newValue : float){
	designOptions[0].SetProceduralFloat("MetalWearRust", newValue);
	designOptions[1].SetProceduralFloat("MetalWearRust", newValue);
	RebuildTextures();
}

// GROUND DIRT
function GroundDirtHeight(newValue : float){
	designOptions[0].SetProceduralFloat("GroundDirtHeight", newValue);
	designOptions[1].SetProceduralFloat("GroundDirtHeight", newValue);
	RebuildTextures();
}

function GroundDirtLevel(newValue : float){
	designOptions[0].SetProceduralFloat("GroundDirtLevel", newValue);
	designOptions[1].SetProceduralFloat("GroundDirtLevel", newValue);
	RebuildTextures();
}

function GroundDirtContrast(newValue : float){
	designOptions[0].SetProceduralFloat("GroundDirtContrast", newValue);
	designOptions[1].SetProceduralFloat("GroundDirtContrast", newValue);
	RebuildTextures();
}

function GroundDirtRoughness(newValue : float){
	designOptions[0].SetProceduralFloat("GroundDirtRoughness", newValue);
	designOptions[1].SetProceduralFloat("GroundDirtRoughness", newValue);
	RebuildTextures();
}

function GroundDirtColor(){
	designOptions[0].SetProceduralColor("GroundDirtColor", Color(parseFloat(groundDirtColor[0].GetComponent(UI.Text).text) / 255,parseFloat(groundDirtColor[1].GetComponent(UI.Text).text) / 255,parseFloat(groundDirtColor[2].GetComponent(UI.Text).text) / 255,parseFloat(groundDirtColor[3].GetComponent(UI.Text).text) / 255));
	designOptions[1].SetProceduralColor("GroundDirtColor", Color(parseFloat(groundDirtColor[0].GetComponent(UI.Text).text) / 255,parseFloat(groundDirtColor[1].GetComponent(UI.Text).text) / 255,parseFloat(groundDirtColor[2].GetComponent(UI.Text).text) / 255,parseFloat(groundDirtColor[3].GetComponent(UI.Text).text) / 255));
	RebuildTextures();
}

// CEILING DIRT
function CeilingDirtHeight(newValue : float){
	designOptions[0].SetProceduralFloat("CeilingDirtHeight", newValue);
	designOptions[1].SetProceduralFloat("CeilingDirtHeight", newValue);
	RebuildTextures();
}

function CeilingDirtLevel(newValue : float){
	designOptions[0].SetProceduralFloat("CeilingDirtLevel", newValue);
	designOptions[1].SetProceduralFloat("CeilingDirtLevel", newValue);
	RebuildTextures();
}

function CeilingDirtContrast(newValue : float){
	designOptions[0].SetProceduralFloat("CeilingDirtContrast", newValue);
	designOptions[1].SetProceduralFloat("CeilingDirtContrast", newValue);
	RebuildTextures();
}

function CeilingDirtRoughness(newValue : float){
	designOptions[0].SetProceduralFloat("CeilingDirtRoughness", newValue);
	designOptions[1].SetProceduralFloat("CeilingDirtRoughness", newValue);
	RebuildTextures();
}

function CeilingDirtColor(){
	designOptions[0].SetProceduralColor("CeilingDirtColor", Color(parseFloat(ceilingDirtColor[0].GetComponent(UI.Text).text) / 255,parseFloat(ceilingDirtColor[1].GetComponent(UI.Text).text) / 255,parseFloat(ceilingDirtColor[2].GetComponent(UI.Text).text) / 255,parseFloat(ceilingDirtColor[3].GetComponent(UI.Text).text) / 255));
	designOptions[1].SetProceduralColor("CeilingDirtColor", Color(parseFloat(ceilingDirtColor[0].GetComponent(UI.Text).text) / 255,parseFloat(ceilingDirtColor[1].GetComponent(UI.Text).text) / 255,parseFloat(ceilingDirtColor[2].GetComponent(UI.Text).text) / 255,parseFloat(ceilingDirtColor[3].GetComponent(UI.Text).text) / 255));
	RebuildTextures();
}

// DIRT
function DirtGrungeAmount(newValue : float){
	designOptions[0].SetProceduralFloat("DirtGrungeAmount", newValue);
	designOptions[1].SetProceduralFloat("DirtGrungeAmount", newValue);
	RebuildTextures();
}

function DirtLevel(newValue : float){
	designOptions[0].SetProceduralFloat("DirtLevel", newValue);
	designOptions[1].SetProceduralFloat("DirtLevel", newValue);
	RebuildTextures();
}

function DirtContrast(newValue : float){
	designOptions[0].SetProceduralFloat("DirtContrast", newValue);
	designOptions[1].SetProceduralFloat("DirtContrast", newValue);
	RebuildTextures();
}

function DirtRoughness(newValue : float){
	designOptions[0].SetProceduralFloat("DirtRoughness", newValue);
	designOptions[1].SetProceduralFloat("DirtRoughness", newValue);
	RebuildTextures();
}

function DirtColor(){
	designOptions[0].SetProceduralColor("DirtColor", Color(parseFloat(dirtColor[0].GetComponent(UI.Text).text) / 255,parseFloat(dirtColor[1].GetComponent(UI.Text).text) / 255,parseFloat(dirtColor[2].GetComponent(UI.Text).text) / 255,parseFloat(dirtColor[3].GetComponent(UI.Text).text) / 255));
	designOptions[1].SetProceduralColor("DirtColor", Color(parseFloat(dirtColor[0].GetComponent(UI.Text).text) / 255,parseFloat(dirtColor[1].GetComponent(UI.Text).text) / 255,parseFloat(dirtColor[2].GetComponent(UI.Text).text) / 255,parseFloat(dirtColor[3].GetComponent(UI.Text).text) / 255));
	RebuildTextures();
}















