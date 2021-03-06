#include "xamarin/xamarin.h"


void xamarin_register_modules_impl ()
{

}

void xamarin_register_assemblies_impl ()
{
	guint32 exception_gchandle = 0;
	xamarin_open_and_register ("Xamarin.Forms.Platform.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("ReactiveUI.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("Xamarin.Forms.CarouselView.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("Acr.UserDialogs.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("Plugin.Geolocator.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("Plugin.Media.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("Lottie.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("MvvmCross.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("MvvmCross.Platform.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("MvvmCross.Binding.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("Xamarin.Auth.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("Facebook.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("Xamarin.Forms.Maps.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("MR.Gestures.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("ImageCircle.Forms.Plugin.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("FAB.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);

}

void xamarin_setup_impl ()
{
	xamarin_gc_pump = TRUE;
	xamarin_init_mono_debug = TRUE;
	xamarin_executable_name = "XamFormsMvxTemplateiOS.exe";
	mono_use_llvm = FALSE;
	xamarin_log_level = 2;
	xamarin_arch_name = "x86_64";
	xamarin_marshal_objectivec_exception_mode = MarshalObjectiveCExceptionModeUnwindManagedCode;
	xamarin_debug_mode = TRUE;
	setenv ("MONO_GC_PARAMS", "nursery-size=512k,major=marksweep", 1);
}

int main (int argc, char **argv)
{
	NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
	int rv = xamarin_main (argc, argv, false);
	[pool drain];
	return rv;
}
void xamarin_initialize_callbacks () __attribute__ ((constructor));
void xamarin_initialize_callbacks ()
{
	xamarin_setup = xamarin_setup_impl;
	xamarin_register_assemblies = xamarin_register_assemblies_impl;
	xamarin_register_modules = xamarin_register_modules_impl;
}
