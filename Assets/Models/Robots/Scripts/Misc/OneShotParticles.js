
function Start () 
{
	yield WaitForSeconds(GetComponent.<ParticleEmitter>().minEnergy / 2);
	GetComponent.<ParticleEmitter>().emit = false;
}